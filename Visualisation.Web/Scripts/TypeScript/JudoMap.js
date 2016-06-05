/// <reference path="./typings/google.maps.d.ts" />
var Mapping;
(function (Mapping) {
    var LatLng = google.maps.LatLng;
    var JudoMap = (function () {
        function JudoMap(mapDiv) {
            var _this = this;
            this.timeoutTimeMs = 5000;
            this.maxLengthOfLabel = 20;
            var mapStyles = [
                {
                    featureType: "all",
                    elementType: "labels",
                    stylers: [
                        { visibility: "off" }
                    ]
                }
            ];
            var latLngForCenter = new LatLng(54.559322, -4.174804);
            var mapOptions = {
                center: latLngForCenter,
                zoom: 6,
                styles: mapStyles,
                draggable: false,
                zoomControl: false,
                scrollwheel: false,
                disableDoubleClickZoom: true,
                streetViewControl: false
            };
            this.map = new google.maps.Map(mapDiv, mapOptions);
            $(window).resize(function () { return _this.map.setCenter(latLngForCenter); });
        }
        JudoMap.prototype.drawOnMap = function (label, lat, long) {
            var point = new LatLng(lat, long);
            var shape = new google.maps.Circle({
                center: point,
                map: this.map,
                radius: 10000,
                visible: true,
                clickable: false,
                draggable: false,
                fillColor: "#ffbe0f",
                strokeColor: "#b3850b",
                strokeWeight: 1
            });
            setTimeout(function () { return shape.setMap(null); }, this.timeoutTimeMs);
            if (label) {
                if (label.length > this.maxLengthOfLabel) {
                    label = label.substr(0, this.maxLengthOfLabel);
                }
                var infoWindow = new google.maps.InfoWindow({
                    disableAutoPan: true,
                    content: label,
                    position: point,
                });
                infoWindow.open(this.map, shape);
                setTimeout(function () { return infoWindow.close(); }, this.timeoutTimeMs);
            }
        };
        return JudoMap;
    }());
    Mapping.JudoMap = JudoMap;
})(Mapping || (Mapping = {}));
//# sourceMappingURL=JudoMap.js.map