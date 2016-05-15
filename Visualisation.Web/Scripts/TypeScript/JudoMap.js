/// <reference path="./typings/google.maps.d.ts" />
var Mapping;
(function (Mapping) {
    var LatLng = google.maps.LatLng;
    var JudoMap = (function () {
        function JudoMap(mapDiv) {
            var _this = this;
            var mapStyles = [
                {
                    featureType: "all",
                    elementType: "labels",
                    stylers: [
                        { visibility: "off" }
                    ]
                }
            ];
            var latLng = new LatLng(54.559322, -4.174804);
            var mapOptions = {
                center: latLng,
                zoom: 6,
                styles: mapStyles,
                draggable: false,
                zoomControl: false,
                scrollwheel: false,
                disableDoubleClickZoom: true,
                streetViewControl: false
            };
            this.map = new google.maps.Map(mapDiv, mapOptions);
            $(window).resize(function () { return _this.map.setCenter(latLng); });
        }
        JudoMap.prototype.drawOnMap = function (label, lat, long) {
            var point = new LatLng(lat, long);
            var shape = new google.maps.Circle({
                center: point,
                map: this.map,
                radius: 7000,
                visible: true,
                clickable: false,
                draggable: false,
                fillColor: "#ffbe0f",
                strokeColor: "#b3850b",
                strokeWeight: 1
            });
            var infoWindow = new google.maps.InfoWindow({
                disableAutoPan: true,
                content: label,
                position: point,
            });
            infoWindow.open(this.map, shape);
            setTimeout(function () {
                shape.setMap(null);
                infoWindow.close();
            }, 5000);
        };
        return JudoMap;
    }());
    Mapping.JudoMap = JudoMap;
})(Mapping || (Mapping = {}));
//# sourceMappingURL=JudoMap.js.map