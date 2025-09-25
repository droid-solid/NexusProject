let tasks = [];
let editingIndex = null;
let chartInstances = {};

// 🧩 Modal Controls
// 📝 Save Task

// 📋 Render Task Table

// 🗂️ Render Kanban View

// 🧱 Render Summary Tiles

// 📊 Render Charts
function renderCharts(filteredData = null) {
  const data = filteredData || {
    completion: [28, 14],
    productivity: [9, 7, 4],
    distribution: [28, 10, 4],
    activity: [3, 5, 4, 6, 2, 7, 5]
  };

  ['completionChart', 'productivityChart', 'distributionChart', 'activityChart'].forEach(id => {
    if (chartInstances[id]) chartInstances[id].destroy();
  });

  chartInstances.completionChart = new Chart(document.getElementById('completionChart'), {
    type: 'doughnut',
    data: {
      labels: ['Completed', 'Remaining'],
      datasets: [{ data: data.completion, backgroundColor: ['#10B981', '#D1D5DB'] }]
    }
  });

  chartInstances.productivityChart = new Chart(document.getElementById('productivityChart'), {
    type: 'bar',
    data: {
      labels: ['Ada', 'Chuka', 'Fatima'],
      datasets: [{ label: 'Tasks Completed', data: data.productivity, backgroundColor: '#3B82F6' }]
    },
    options: { scales: { y: { beginAtZero: true } } }
  });

  chartInstances.distributionChart = new Chart(document.getElementById('distributionChart'), {
    type: 'pie',
    data: {
      labels: ['Completed', 'In Progress', 'Pending'],
      datasets: [{ data: data.distribution, backgroundColor: ['#10B981', '#FBBF24', '#EF4444'] }]
    }
  });

  chartInstances.activityChart = new Chart(document.getElementById('activityChart'), {
    type: 'line',
    data: {
      labels: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
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
}

// 📅 Filter Charts by Date
function filterCharts() {
  const start = document.getElementById('startDate').value;
  const end = document.getElementById('endDate').value;

  // Simulated filter logic
  const filteredData = {
    completion: [20, 22],
    productivity: [5, 3, 2],
    distribution: [20, 15, 7],
    activity: [1, 2, 3, 2, 1, 4, 2]
  };

  renderCharts(filteredData);
}

// 🚀 Initialize
window.onload = () => {
  renderCharts();
};