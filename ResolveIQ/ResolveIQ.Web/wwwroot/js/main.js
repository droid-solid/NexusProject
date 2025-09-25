let chartInstances = {};

// 🧩 Modal Controls
// 📝 Save Task

// 📋 Render Task Table

// 🗂️ Render Kanban View

// 🧱 Render Summary Tiles

// 📊 Render Charts
function renderCharts() {

    var startDate = $("#startDate").val();
    var endDate = $("#endDate").val()
    var chartMode = $("#chartMode").val();

    var productivitydata = [];
    var productivityLabels = [];

    var taskDistribution = [];
    var taskDistributionLabel = [];

    var completionData = [];
    var completionLabel = [];

    var weeklyCompletion = [];
    var weeklyCompletionLabel = [];

    console.log(startDate, endDate, chartMode);
    $.get("/Home/GetReportData?startDate=" + startDate + "&endDate=" + endDate + "&chartMode=" + chartMode, function (response) {

        for (let key in response.productivityData) {
            productivityLabels.push(key);
            productivitydata.push(response.productivityData[key]);
        }

        for (let key in response.taskDistribution) {
            taskDistributionLabel.push(key);
            taskDistribution.push(response.taskDistribution[key]);
        }

        for (let key in response.completionData) {
            completionLabel.push(key);
            completionData.push(response.completionData[key]);
        }

        for (let key in response.weeklyCompletion) {
            weeklyCompletionLabel.push(key);
            weeklyCompletion.push(response.weeklyCompletion[key]);
        }


        let data = {
            completion: completionData,
            productivity: productivitydata,
            distribution: taskDistribution,
            activity: weeklyCompletion
        };


        ['completionChart', 'productivityChart', 'distributionChart', 'activityChart'].forEach(id => {
            if (chartInstances[id]) chartInstances[id].destroy();
        });

        chartInstances.completionChart = new Chart(document.getElementById('completionChart'), {
            type: 'doughnut',
            data: {
                labels: completionLabel,
                datasets: [{ data: data.completion, backgroundColor: ['#10B981', '#D1D5DB'] }]
            }
        });

        chartInstances.productivityChart = new Chart(document.getElementById('productivityChart'), {
            type: 'bar',
            data: {
                labels: productivityLabels,
                datasets: [{ label: 'Tasks Completed', data: data.productivity, backgroundColor: '#3B82F6' }]
            },
            options: { scales: { y: { beginAtZero: true } } }
        });

        chartInstances.distributionChart = new Chart(document.getElementById('distributionChart'), {
            type: 'pie',
            data: {
                labels: taskDistributionLabel,
                datasets: [{ data: data.distribution, backgroundColor: ['#10B981', '#FBBF24', '#EF4444'] }]
            }
        });

        chartInstances.activityChart = new Chart(document.getElementById('activityChart'), {
            type: 'line',
            data: {
                labels: weeklyCompletionLabel,
                datasets: [{
                    label: 'Tasks Updated',
                    data: data.activity,
                    borderColor: '#6366F1',
                    backgroundColor: 'rgba(99, 102, 241, 0.2)',
                    fill: true,
                    tension: 0.4
                }]
            }
        });        
    });

  
}

// 📅 Filter Charts by Date
function filterCharts() {
 /* const start = document.getElementById('startDate').value;
  const end = document.getElementById('endDate').value;

  // Simulated filter logic
  const filteredData = {
    completion: [20, 22],
    productivity: [5, 3, 2],
    distribution: [20, 15, 7],
    activity: [1, 2, 3, 2, 1, 4, 2]
  };*/

  renderCharts();
}

// 🚀 Initialize
window.onload = () => {
  renderCharts();
};