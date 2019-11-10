$(function () {
    ko.applyBindings(resultsModel);
});

var resultsModel = {
    url: ko.observable(),
    keywords: ko.observable(),
    Paragraphs: ko.observableArray(),
    showErrorMsg: ko.observable(false),
    getResults: function ()
    {
        if (!this.keywords() && !this.url()) {
            this.showErrorMsg(true);
            return;
        } 
        var thisObj = this;
        try {
            $.ajax({
                url: '/Home/GetResultsAsync',
                type: 'post',
                dataType: 'json',
                data: ko.toJSON({ url: thisObj.url, keywords: thisObj.keywords }), 
                contentType: 'application/json',
                success: function (data) {
                    thisObj.Paragraphs(data);
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

