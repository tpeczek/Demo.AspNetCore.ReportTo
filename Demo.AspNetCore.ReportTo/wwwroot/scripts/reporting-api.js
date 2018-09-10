var ReportingApi = (function () {
    triggerDeprecation = function () {
        // Synchronous XHR
        var xhr = new XMLHttpRequest();
        xhr.open('GET', '/xhr', false);
        xhr.send();
    };

    return {
        initialize: function () {
            triggerDeprecation();
        }
    };
})();

ReportingApi.initialize();