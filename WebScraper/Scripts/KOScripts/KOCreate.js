$(function () {
    ko.applyBindings(resultsModel);
});

var resultsModel = {
    url: ko.observable(),
    keywords: ko.observable(),
    searchNumberResults: ko.observable(),
    paragraphs: ko.observableArray(),
    showErrorMsg: ko.observable(false),
    returnMessage: ko.observable(),
    getResults: function ()
    {
        if (!this.url()) {
            this.showErrorMsg(true);
            return;
        } 
        var thisObj = this;
        try {
            $.ajax({
                url: '/Home/GetResultsAsync',
                type: 'post',
                dataType: 'json',
                data: ko.toJSON({ searchUrl: thisObj.url, keywords: thisObj.keywords, searchNumberResults: thisObj.searchNumberResults }), 
                contentType: 'application/json',
                success: function (data) {
                    thisObj.paragraphs(data);
                    debugger;
                    if (thisObj.paragraphs()[0] == "0")
                        thisObj.returnMessage("No results where found for given url")
                    else
                        thisObj.returnMessage("Found given url in positions: ")
                },
                error: errorCallback
            });
        } catch (e) {
            console.log(e);
        }
    }
};

function errorCallback(err) {
    console.log(err);
}

