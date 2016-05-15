/// <reference path="./typings/google.maps.d.ts" />


module Mapping {
    import LatLng = google.maps.LatLng;
    import MapTypeStyle = google.maps.MapTypeStyle
    import MapOptions = google.maps.MapOptions;

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
            const latLng = new LatLng(54.559322, -4.174804 );
            var mapOptions: MapOptions = {
                center: latLng,
                zoom: 6,
                styles: mapStyles,
                draggable: false,
                zoomControl: false,
                scrollwheel: false,
                disableDoubleClickZoom: true,
                streetViewControl: false
            }

            this.map = new google.maps.Map(mapDiv, mapOptions);
            $(window).resize(() => this.map.setCenter(latLng));
        }

        drawOnMap(label:string, lat: number, long: number) {
            const point = new LatLng(lat, long);
            
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

            setTimeout(() => {
                shape.setMap(null);
                infoWindow.close();
            }, 5000);
        }
    }
}
