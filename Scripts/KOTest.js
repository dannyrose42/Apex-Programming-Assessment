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
//Initialize  Datepickers

$("#startDate").datepicker();
$("#startDate").datepicker("setDate", "03/30/2014");
$("#endDate").datepicker();
$("#endDate").datepicker("setDate", "04/01/2014");

//Wire up submit button
$("#submitBtn").click(function (e) {
    e.preventDefault();
    UpdateGrid();
});


$("#startDate").datepicker();
$("#startDate").datepicker("setDate", "03/30/2014");
$("#endDate").datepicker();
$("#endDate").datepicker("setDate", "04/01/2014");

UpdateGrid();