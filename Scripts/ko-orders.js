//Set Knockout View Mode
function ViewModel() {
    var self = this;

    self.data = ko.observableArray();
};
var viewModel = new ViewModel();
ko.applyBindings(viewModel);

function UpdateGrid() {
    var sDate = $("#startDate").datepicker("getDate").toLocaleDateString();
    var eDate = $("#endDate").datepicker("getDate").toLocaleDateString();
    $.ajax({
        type: "GET",
        url: "api/values",
        data: {
            startDate: sDate,
            endDate: eDate
        },
        success: function (data) {
            viewModel.data(data);
        },
        error: function (data) {
            alert('error');
            console.log(data);
        }
    });
}

//Wire up submit button
$("#submitBtn").click(function (e) {
    e.preventDefault();
    UpdateGrid();
});
//Since we can't return the excel file via ajax, we'll make just use ajax to tell
//the server to make the file and hold on it. Then on success we'll ask the server to 
//give it to us.
$("#exportBtn").click(function (e) {
    e.preventDefault();
    var sDate = $("#startDate").datepicker("getDate").toLocaleDateString();
    var eDate = $("#endDate").datepicker("getDate").toLocaleDateString();
    $.ajax({
        type: "GET",
        url: "Export/RequestExcelDownload",
        data: {
            startDate: sDate,
            endDate: eDate
        },
        success: function (data) {            
            window.location = 'Export/Download?fileGuid=' + data.FileGuid
                + '&filename=' + data.FileName;
        },
        error: function (data) {
            alert('error');
            console.log(data);
        }
    });

});



var date = new Date(), y = date.getFullYear(), m = date.getMonth();
//Default start to beginning of previous month, and default end to the ending of the previous month
var startDate = new Date(y, m - 1, 1);
var endDate = new Date(y, m, 0);
$("#startDate").datepicker();
$("#startDate").datepicker("setDate", startDate);

$("#endDate").datepicker();
$("#endDate").datepicker("setDate", endDate);
//Inital Grid Load
UpdateGrid();