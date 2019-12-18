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
            //var response = JSON.parse(data);
            window.location = 'Export/Download?fileGuid=' + data.FileGuid
                + '&filename=' + data.FileName;
        },
        error: function (data) {
            alert('error');
            console.log(data);
        }
    });
    
    //fetch('api/values', { startDate: sDate, endDate: eDate, getSpreadSheet:true})
    //    .then(resp => resp.blob())
    //    .then(blob => {
    //        const url = window.URL.createObjectURL(blob);
    //        const a = document.createElement('a');
    //        a.style.display = 'none';
    //        a.href = url;
    //        // the filename you want
    //        a.download = 'todo-1.xls';
    //        document.body.appendChild(a);
    //        a.click();
    //        window.URL.revokeObjectURL(url);
    //        alert('your file has downloaded!'); // or you know, something with better UX...
    //    })
    //    .catch(() => alert('oh no!'));
});


$("#startDate").datepicker();
$("#startDate").datepicker("setDate", "03/30/2014");
$("#endDate").datepicker();
$("#endDate").datepicker("setDate", "04/01/2014");

UpdateGrid();