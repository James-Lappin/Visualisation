/// <reference path="./typings/google.maps.d.ts" />
var Mapping;
(function (Mapping) {
    var LatLng = google.maps.LatLng;
    var Marker = google.maps.Marker;
    var JudoMap = (function () {
        function JudoMap(mapDiv) {
            var mapStyles = [
                {
                    featureType: "all",
                    elementType: "labels",
                    stylers: [
                        { visibility: "off" }
                    ]
                }
            ];
            var mapOptions = {
                center: new LatLng(54.559322, -4.174804),
                zoom: 6,
                styles: mapStyles,
                draggable: false,
                zoomControl: false,
                scrollwheel: false,
                disableDoubleClickZoom: true
            };
            this.map = new google.maps.Map(mapDiv, mapOptions);
        }
        JudoMap.prototype.drawOnMap = function (label, lat, long) {
            var point = new LatLng(parseFloat(lat), parseFloat(long));
            var markerLabel = {
                text: label
            };
            var marker = new Marker({
                map: this.map,
                position: point,
                label: markerLabel,
                draggable: false,
                clickable: false
            });
            setTimeout(function () {
                marker.setMap(null);
            }, 5000);
        };
        return JudoMap;
    }());
    Mapping.JudoMap = JudoMap;
})(Mapping || (Mapping = {}));
//# sourceMappingURL=JudoMap.js.map