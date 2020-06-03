// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = 'Nunito', '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#858796';
$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/Chart/GetProducts",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: data_arrived
    });
})
function data_arrived(data) {
    console.log(data);
    // Pie Chart Example
    var x_data = data[0];
    var y_data = data[1];
    var graph_data = {
        labels: x_data,
        datasets: [
            {
                data: y_data,
                backgroundColor: ['#4e73df', '#1cc88a', '#36b9cc'],
                hoverBackgroundColor: ['#2e59d9', '#17a673', '#2c9faf'],
                hoverBorderColor: "rgba(234, 236, 244, 1)",
            }
        ]

    };
    var panel1 = $('#pieChart').get(0).getContext("2d");
    var myPieChart = new Chart(panel1, {
        type: 'pie',
        data: graph_data,
        options: {
            maintainAspectRatio: false,
            tooltips: {
                backgroundColor: "rgb(255,255,255)",
                bodyFontColor: "#858796",
                borderColor: '#dddfeb',
                borderWidth: 1,
                xPadding: 15,
                yPadding: 15,
                displayColors: false,
                caretPadding: 10,
            },
            legend: {
                display: false
            },
            cutoutPercentage: 80,
        },
    });
}

    //$(document).ready(function () {
    //    $.ajax({
    //        type: "GET",
    //        url: "/Chart/GetProducts",
    //        contentType: "application/json;charset=utf-8",
    //        datatype: "json",
    //        success: data_arrived
    //    });
    //    })
    //    function data_arrived(data) {
    //    console.log(data);
    //        var x_data = data[0];
    //        var y_data = data[1];
    //        var graph_data = {
    //    labels: x_data,
    //            datasets: [
    //                {
    //    data: y_data,
    //                    backgroundColor: ['#4e73df', '#1cc88a', '#36b9cc'],
    //                    hoverBackgroundColor: ['#2e59d9', '#17a673', '#2c9faf'],
    //                    hoverBorderColor: "rgba(234, 236, 244, 1)",
    //                }
    //            ]

    //        };
    //        var panel1 = $('#pieChart').get(0).getContext("2d");
    //        var pieChart = new Chart(
    //            panel1,
    //            {
    //    type: 'pie',
    //                data: graph_data,
    //                options: {
    //    maintainAspectRatio: false
    //                }
    //            }
    //        );
    //    }

 