import { redirect } from 'next/navigation';
import { useState } from 'react';
import toast from 'react-hot-toast';

export function useGeolocation() {
  const [positions, setPosition] = useState<{
    lat: number;
    lng: number;
  } | null>(null);
  const [loading, setLoader] = useState<boolean>(false);

  const getPosition = () => {
    if (!navigator.geolocation) {
      toast.error('Geolocation is not supported by your browser.');
      return;
    }
    setLoader(true);
    navigator.geolocation.getCurrentPosition(
      res => {
        const { latitude, longitude } = res.coords;
        setPosition({ lat: latitude, lng: longitude });
        setLoader(false);
        redirect(`/apps/form?lat=${latitude}&lng=${longitude}`);
      },
      err => {
        setLoader(false);
        console.log(err);
      },
      {
        enableHighAccuracy: false, // جرب false أولاً
        timeout: 20000, // 20 ثانية بدل 10
        maximumAge: 10000, // السماح باستخدام الموقع المخزن لمدة 10 ثواني
      }
    );
  };
  return { positions, loading, getPosition };
}
