/* eslint-disable react-hooks/set-state-in-effect */
'use client';
import L, { LatLngExpression } from 'leaflet';
import {
  MapContainer,
  Marker,
  TileLayer,
  useMap,
  useMapEvents,
} from 'react-leaflet';
import 'leaflet/dist/leaflet.css';
import { OpenStreetMapProvider, GeoSearchControl } from 'leaflet-geosearch';
import 'leaflet-geosearch/dist/geosearch.css';

import markerIcon2x from '@/public/marker-icon-2x.png';
import markerIcon from '@/public//marker-icon.png';
import markerShadow from '@/public/marker-shadow.png';
import { useEffect, useState } from 'react';
import { redirect, useRouter } from 'next/navigation';
import { usePosition } from '../_hooks/usePosition';

import { useGeolocation } from '../_hooks/useGeolocation';

L.Icon.Default.mergeOptions({
  iconRetinaUrl: markerIcon2x.src,
  iconUrl: markerIcon.src,
  shadowUrl: markerShadow.src,
});

export default function Map() {
  const [position, setPosition] = useState<LatLngExpression>([51.505, -0.09]);
  const { lat, lng } = usePosition();

  const { positions, getPosition, loading } = useGeolocation();

  useEffect(() => {
    if (!lat || !lng) return;
    setPosition([+lat, +lng]);
  }, [lat, lng]);

  return (
    <div className="h-full flex-1 relative">
      {!positions && (
        <button
          onClick={getPosition}
          className="bg-brand--2 rounded-[7px] border-none px-5 py-2 cursor-pointer hover:opacity-90 transition text-base capitalize font-semibold absolute bottom-10 left-[50%] z-999 -translate-x-[50%]"
        >
          {loading ? 'Loading...' : 'get position'}
        </button>
      )}
      <MapContainer
        center={position}
        zoom={13}
        scrollWheelZoom={true}
        className="h-full"
        closePopupOnClick={true}
      >
        <TileLayer
          attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
          url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
        />
        <Marker position={position} />

        <LocationMarker />
        <ChangeCenter position={position} />
        <SearchControl />
      </MapContainer>
    </div>
  );
}
function LocationMarker() {
  useMapEvents({
    click(e) {
      redirect(`/apps/form?lat=${e.latlng.lat}&lng=${e.latlng.lng}`);
    },
  });

  return null;
}
function ChangeCenter({ position }: { position: LatLngExpression }) {
  const map = useMap();
  map.setView(position);
  return null;
}

function SearchControl() {
  const map = useMap();
  const router = useRouter();
  useEffect(() => {
    const provider: OpenStreetMapProvider = new OpenStreetMapProvider();
    const searchControl = GeoSearchControl({
      notFoundMessage: 'Sorry, that address could not be found.',
      provider,
      style: 'button',
      showMarker: false,
      showPopup: true,
      autoClose: false,
      retainZoomLevel: false,
      animateZoom: true,
      keepResult: true,
    });

    map.on('geosearch/showlocation', (e: any) => {
      const lng = e?.location?.x;
      const lat = e?.location?.y;
      router.push(`/apps/form?lat=${lat}&lng=${lng}`);
    });

    map.addControl(searchControl);
    return () => {
      map.removeControl(searchControl);
      map.off('geosearch/showlocation');
    };
  }, [map, router]);

  return null;
}
