// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.getElementById('toggle-filters').addEventListener('click', () => {
    const panel = document.getElementById('filters-panel');
    panel.classList.toggle('open');
});

document.getElementById('clear-filters').addEventListener('click', () => {
    const checkboxes = document.querySelectorAll('#filters-form input[type="checkbox"]');
    checkboxes.forEach(checkbox => checkbox.checked = false);
});