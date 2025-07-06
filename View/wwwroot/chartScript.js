window.renderBarChart = (canvasId, labels, data) => {
    const ctx = document.getElementById(canvasId).getContext('2d');

    if (window.existingChart) {
        window.existingChart.destroy(); // avoid duplicates
    }

    window.existingChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Revenue (PKR)',
                data: data,
                backgroundColor: 'rgba(204, 85, 0, 0.6)',
                borderColor: 'rgba(153, 51, 0, 1)',
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
};

function forceLogoutAndReload() {
    window.location.href = "/";
    location.reload(true); // full page reload from server
}
