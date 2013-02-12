/// <reference path="jquery-1.9.1.intellisense.js" />
/// <reference path="GeoMap.js" />

$(document).ready(function () {
	GeoMap.Initialize();
	GeoMap.Show();

	$.ajaxSetup({
		cache: false,
		error: function (err, y) { alert(err.responseText); },
		type: "POST",
		dataType: "json",
	});

	$("#btnStates").click(function () {
		$.ajax({
			data: {},
			success: GeoMap.ReceiveData,
			url: "../Map/States"
		});
	});

	$("#btnCities").click(function () {
		$.ajax({
			data: {},
			success: GeoMap.ReceiveData,
			url: "../Map/Cities"
		});
	});
});
