/// <reference path="./typings/google.maps.d.ts" />


module Mapping {
    import LatLng = google.maps.LatLng;
    import MapTypeStyle = google.maps.MapTypeStyle
    import MapOptions = google.maps.MapOptions;
    import Marker = google.maps.Marker;

    export class JudoMap {
        private map: google.maps.Map;

        constructor(mapDiv: Element) {
            var mapStyles: MapTypeStyle[] = [
                {
                    featureType: "all",
                    elementType: "labels",
                    stylers: [
                        { visibility: "off" }
                    ]
                }
            ];
            
            var mapOptions: MapOptions = {
                center: new LatLng(54.559322, -4.174804 ),
                zoom: 6,
                styles: mapStyles,
                draggable: false,
                zoomControl: false,
                scrollwheel: false,
                disableDoubleClickZoom: true
            }

            this.map = new google.maps.Map(mapDiv, mapOptions);
        }

        drawOnMap(label, lat, long) {
            const point = new LatLng(parseFloat(lat), parseFloat(long));
            var markerLabel: google.maps.MarkerLabel = {
                text: label
            };

            var marker = new Marker({
                map: this.map,
                position: point,
                label: markerLabel,
                draggable: false,
                clickable: false            
            });
            
            setTimeout(() => {
                marker.setMap(null);
            }, 5000);
        }
    }
}
