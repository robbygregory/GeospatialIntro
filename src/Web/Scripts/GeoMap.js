/// <reference path="jquery-1.9.1.intellisense.js" />

var GeoMap = {

	map: null,
	layerEntities: null,
	itemEntities: null,

	GetPushpinOptions: function (image) {
		var options = {
			visible: true,
			icon: image
		}
		return options;
	},

	GetPolygonOptions: function (color) {
		var mapcolor;
		switch (color) {
			case "red": mapcolor = new Microsoft.Maps.Color(100, 255, 0, 0); break;
			case "yellow": mapcolor = new Microsoft.Maps.Color(100, 255, 255, 0); break;
			case "green": mapcolor = new Microsoft.Maps.Color(100, 0, 255, 0); break;
			default: mapcolor = new Microsoft.Maps.Color(100, 0, 0, 255);
		}
		var options = {
			strokeColor: mapcolor,
			strokeWidth: 3,
			fillColor: mapcolor
		};
		return options;
	},

	Initialize: function () {
		var mm = Microsoft.Maps;
		var mapOptions = {
			credentials: "AmhE_lRLaexrUfTQhtrLzUuQgJaftVmDRzrkwRIIbxRkLDRyeRYrUqD5Dv_zPkLt",
			center: new mm.Location(35, -96),
			mapTypeId: mm.MapTypeId.road,
			zoom: 4,
			showScalebar: true,
			width: 300,
			height: 300,
			showCopyright: false,
			enableSearchLogo: false,
			enableClickableLogo: false,
			showDashboard: false,
			showBreadcrumb: true
		};

		Microsoft.Maps.Events.addHandler(GeoMap.map, "viewchangeend", function () {
			console.log("Zoom Level:" + GeoMap.map.getZoom());
			console.log("Map Center:" + GeoMap.map.getCenter());
		});

		GeoMap.map = new mm.Map(document.getElementById("Map"), mapOptions);
		$(window).resize(GeoMap.ResizeMap);

		// we have layers and items, and we handle them separately
		GeoMap.layerEntities = new mm.EntityCollection();
		GeoMap.map.entities.push(GeoMap.layerEntities);
		GeoMap.itemEntities = new mm.EntityCollection();
		GeoMap.map.entities.push(GeoMap.itemEntities);
		GeoMap.localEntities = new mm.EntityCollection();
		GeoMap.map.entities.push(GeoMap.localEntities);
		GeoMap.Show();

		if (navigator.geolocation) {
			navigator.geolocation.getCurrentPosition(function (position) {
				GeoMap.ShowCurrentLocation(position);
			});
		}
	},

	ShowCurrentLocation: function (position) {
		var location = new Microsoft.Maps.Location(position.coords.latitude, position.coords.longitude);
		var pin = new Microsoft.Maps.Pushpin(location, GeoMap.GetPushpinOptions("orange"));
		try {
			GeoMap.localEntities.push(pin);
		} catch (err) { alert(err); }
	},

	ZoomTo: function (lat, long, zoom) {
		var options = { center: new Microsoft.Maps.Location(lat, long), zoom: zoom };
		GeoMap.map.setView(options);
	},

	ReceiveData: function (data, status, xhr) {
		if (data.error) {
			alert(data.error);
			return;
		}
		if (!data["Features"]) {
			return;
		}
		GeoMap.DrawFeatureCollection(data);
	},

	DrawFeatureCollection: function (featureCollection) {
		if (featureCollection) {
			for (var i = 0; i < featureCollection.Features.length; i++) {
				var feature = featureCollection.Features[i];
				switch (feature.Geometry.Type) {
					case "MultiPolygon": GeoMap.DrawMultiPolygon(feature); break;
					case "Point": GeoMap.DrawPoint(feature); break;
					case "Polygon": GeoMap.DrawPolygon(feature); break;
				}
			}
		}
	},

	DrawMultiPolygon: function (feature) {
		if (feature) {
			var mm = Microsoft.Maps;
			$.each(feature.Geometry.Coordinates, function (index, poly) {
				var locations = [];
				$.each(poly, function (cindex, coord) {
					var location = new mm.Location(parseFloat(coord[0]), parseFloat(coord[1]));
					locations.push(location);
				});
				var polygon = new mm.Polygon(locations, GeoMap.GetPolygonOptions(feature.Properties.color));
				polygon["feature"] = feature;
				mm.Events.addHandler(polygon, "click", GeoMap.ShowProperties);
				try {
					GeoMap.layerEntities.push(polygon);
				} catch (err) { }
			});
		}
	},

	DrawPolygon: function (feature) {
		if (feature) {
			var mm = Microsoft.Maps;
			var locations = [];
			$.each(feature.Geometry.Coordinates, function (cindex, coord) {
				var location = new mm.Location(parseFloat(coord[0]), parseFloat(coord[1]));
				locations.push(location);
			});
			var polygon = new mm.Polygon(locations, GeoMap.GetPolygonOptions(feature.Properties.color));
			polygon["feature"] = feature;
			mm.Events.addHandler(polygon, "click", GeoMap.ShowProperties);
			try {
				GeoMap.layerEntities.push(polygon);
			} catch (err) { }
		}
	},

	DrawPoint: function (feature) {
		if (feature) {
			var mm = Microsoft.Maps;
			var location = new mm.Location(
				parseFloat(feature.Geometry.Coordinates[0]),
				parseFloat(feature.Geometry.Coordinates[1]));
			var pin = new mm.Pushpin(location, GeoMap.GetPushpinOptions(feature.Properties.image));
			pin["feature"] = feature;
			mm.Events.addHandler(pin, "click", GeoMap.ShowProperties);
			try {
				GeoMap.itemEntities.push(pin);
			} catch (err) { }
		}
	},

	// The callback argument is a Microsoft.Maps.MouseEventArgs  
	ShowProperties: function (mea) {
		var $props = $("#Properties");
		var properties = mea.target.feature.Properties;
		var target = mea.target;
		$props.hide('slow', function () {
			$.when($("tbody tr", $props).remove()).done(function () {
				$.each(properties, function (key, value) {
					if (key != "color") {
						var row = $('<tr><td>' + key + '</td><td>' + value + '</td></tr>');
						$("tbody", $props).append(row);
					}
				});
			});
		});

		$props.show('slow', function () {
			// Animation complete.
		});
	},

	ClearProperties: function () {
		var $props = $("#Properties");
		$.when(GeoMap.HideProperties()).done(function () {
			$("tbody tr", $props).remove();
		});
	},

	HideProperties: function () {
		var $props = $("#Properties");
		$props.hide('slow', function () {
			// Animation complete.
		});
	},

	Show: function () {
		if (GeoMap.map == null) {
			return;
		}
		GeoMap.ResizeMap();
	},

	GetBounds: function () {
		var bounds = GeoMap.map.getBounds();
		var box = {};
		box.North = bounds.getNorth();
		box.South = bounds.getSouth();
		box.East = bounds.getEast();
		box.West = bounds.getWest();
		return box;
	},

	ResizeMap: function () {
		if (GeoMap.map == null) {
			return;
		}
		var height = $(window).height() - $("body").height() + GeoMap.map.getHeight();
		GeoMap.map.setOptions({
			height: $("#MapPanel").height(),
			width: $("#MapPanel").width()
		});
	}
};