let tasks = [];
let editingIndex = null;
let chartInstances = {};

// 🧩 Modal Controls
function openModal(index = null) {
  document.getElementById('taskModal').classList.remove('hidden');
  document.getElementById('taskForm').reset();
  editingIndex = index;

  if (index !== null) {
    const task = tasks[index];
    document.getElementById('modalTitle').textContent = 'Edit Task';
    document.getElementById('taskTitle').value = task.title;
    document.getElementById('taskDescription').value = task.description;
    document.getElementById('taskDueDate').value = task.dueDate;
    document.getElementById('taskStatus').value = task.status;
  } else {
    document.getElementById('modalTitle').textContent = 'Add Task';
  }
}

function closeModal() {
  document.getElementById('taskModal').classList.add('hidden');
}

// 📝 Save Task
function saveTask(e) {
  e.preventDefault();
  const task = {
    title: document.getElementById('taskTitle').value,
    description: document.getElementById('taskDescription').value,
    dueDate: document.getElementById('taskDueDate').value,
    status: document.getElementById('taskStatus').value,
    assignee: 'Unassigned'
  };

  if (editingIndex !== null) {
    tasks[editingIndex] = task;
  } else {
    tasks.push(task);
  }

  renderTasks();
  renderKanban();
  renderTiles();
  closeModal();
}

// 📋 Render Task Table
function renderTasks() {
  const tbody = document.getElementById('taskTableBody');
  tbody.innerHTML = '';
  tasks.forEach((task, index) => {
    const row = document.createElement('tr');
    row.className = 'border-t';
    row.innerHTML = `
      <td class="px-4 py-2">${task.title}</td>
      <td class="px-4 py-2">${task.status}</td>
      <td class="px-4 py-2">${task.dueDate}</td>
      <td class="px-4 py-2">${task.assignee}</td>
      <td class="px-4 py-2">
        <button onclick="openModal(${index})" class="text-blue-600 hover:underline">Edit</button>
      </td>
    `;
    tbody.appendChild(row);
  });
}

// 🗂️ Render Kanban View
function renderKanban() {
  const kanban = {
    'Pending': [],
    'In Progress': [],
    'Completed': []
  };

  tasks.forEach(task => {
    kanban[task.status]?.push(task);
  });

  const kanbanView = document.getElementById('kanbanView');
  kanbanView.innerHTML = '';

  Object.keys(kanban).forEach(status => {
    const column = document.createElement('div');
    column.className = 'bg-white p-4 rounded shadow';
    column.innerHTML = `<h3 class="text-lg font-bold mb-4">${status}</h3>`;
    kanban[status].forEach(task => {
      const card = document.createElement('div');
      card.className = `p-3 rounded mb-2 ${status === 'Completed' ? 'bg-green-100' : status === 'In Progress' ? 'bg-yellow-100' : 'bg-gray-100'}`;
      card.textContent = task.title;
      column.appendChild(card);
    });
    kanbanView.appendChild(column);
  });
}

// 🧱 Render Summary Tiles
function renderTiles() {
  const total = tasks.length;
  const completed = tasks.filter(t => t.status === 'Completed').length;
  const inProgress = tasks.filter(t => t.status === 'In Progress').length;
  const pending = tasks.filter(t => t.status === 'Pending').length;

  const tiles = document.getElementById('tiles');
  tiles.innerHTML = `
    <div class="bg-white p-6 rounded shadow text-center">
      <h3 class="text-sm text-gray-500">Total Tasks</h3>
      <p class="text-3xl font-bold text-blue-600">${total}</p>
    </div>
    <div class="bg-white p-6 rounded shadow text-center">
      <h3 class="text-sm text-gray-500">Completed</h3>
      <p class="text-3xl font-bold text-green-600">${completed}</p>
    </div>
    <div class="bg-white p-6 rounded shadow text-center">
      <h3 class="text-sm text-gray-500">In Progress</h3>
      <p class="text-3xl font-bold text-yellow-500">${inProgress}</p>
    </div>
    <div class="bg-white p-6 rounded shadow text-center">
      <h3 class="text-sm text-gray-500">Pending</h3>
      <p class="text-3xl font-bold text-red-500">${pending}</p>
    </div>
  `;
}

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
  renderTasks();
  renderKanban();
  renderTiles();
  renderCharts();
};