'use client';

import dynamic from 'next/dynamic';

const Map = dynamic(() => import('@/app/_components/Map'), {
  ssr: false,
});

export default function MapWrapper() {
  return (
    <>
      <Map />
    </>
  );
}
