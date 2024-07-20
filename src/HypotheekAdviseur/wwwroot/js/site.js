// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// An enhanced load allows users to navigate between different pages
Blazor.addEventListener("enhancedload", function () {
    // HTMX need to reprocess any htmx tags because of enhanced loading
    htmx.process(document.body);
});