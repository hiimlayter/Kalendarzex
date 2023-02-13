// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    var PlaceHolderElement = $('#PlaceHolderHere');
    $('button[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal('show');
        })
    })

    PlaceHolderElement.on('click', '[data-save="modal"]', function (event) {
        PlaceHolderElement.find('.modal').modal('hide');
    })

    var PlaceHolderDetails = $('#PlaceHolderDetails');
    $('button[data-toggle="detail-modal"]').click(function (event) {
        var url = $(this).data('url');
        var decodeUrl = decodeURIComponent(url);
        $.get(decodeUrl).done(function (data) {
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal('show');
        })
    })

    PlaceHolderDetails.on('click', '[data-save="modal"]', function (event) {
        PlaceHolderElement.find('.modal').modal('hide');
    })

    var NoteHere = $('#NoteHere');
    $('button[data-toggle="note-modal"]').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            NoteHere.html(data);
            NoteHere.find('.modal').modal('show');
        })
    })

    NoteHere.on('click', '[data-save="modal"]', function (event) {
        NoteHere.find('.modal').modal('hide');
    })

    var NoteDetails = $('#NoteDetails');
    $('button[data-toggle="notedet-modal"]').click(function (event) {
        var url = $(this).data('url');
        var decodeUrl = decodeURIComponent(url);
        $.get(decodeUrl).done(function (data) {
            NoteDetails.html(data);
            NoteDetails.find('.modal').modal('show');
        })
    })

    NoteDetails.on('click', '[data-save="modal"]', function (event) {
        NoteDetails.find('.modal').modal('hide');
    })
})
