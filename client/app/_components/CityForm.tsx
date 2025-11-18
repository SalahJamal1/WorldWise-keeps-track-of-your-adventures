'use client';

import { useRouter } from 'next/navigation';
import { useEffect } from 'react';
import { AddCity, apiCity, GetCities } from '../_lib/apiCity';
import { usePosition } from '../_hooks/usePosition';
import { useAuth } from '../_hooks/useAuth';
import { SubmitHandler, useForm } from 'react-hook-form';
import { convertToEmoji } from '../utils/helper';
import { ICITY } from '../utils/ICITY';

export default function CityForm() {
  const { dispatch } = useAuth()!;
  const router = useRouter();
  const { watch, setValue, register, handleSubmit } = useForm<ICITY>({
    defaultValues: {
      cityName: '',
      country: '',
      emoji: '',
      date: new Date().toISOString().split('T')[0],
      notes: '',
      lat: 0,
      lng: 0,
    },
  });
  const { lat, lng } = usePosition();
  useEffect(() => {
    const controller = new AbortController();
    if (lat && lng) {
      (async () => {
        try {
          const res = await apiCity(lat, lng, controller.signal);
          setValue('cityName', res.data.locality || res.data.city);
          setValue('emoji', convertToEmoji(res.data.countryCode));
          setValue('country', res.data.countryName);
          setValue('lat', +lat);
          setValue('lng', +lng);
        } catch (err: any) {
          console.log(err);
        }
      })();
    }
    return () => controller.abort();
  }, [lat, lng]);

  const onSubmit: SubmitHandler<ICITY> = async value => {
    dispatch({ type: 'LOADER' });
    try {
      const res = await AddCity(value);
      const data = await GetCities();
      dispatch({ type: 'LOAD_CITIES', payload: data?.data });
    } catch (err) {
      console.log(err);
    }
    router.push('/apps/cities');
  };
  const emoji = watch('emoji');
  const cityName = watch('cityName');
  return (
    <form
      onSubmit={handleSubmit(onSubmit)}
      className="flex flex-col bg-dark--2 rounded-[5px] space-y-8"
    >
      <div className="flex flex-col gap-1">
        <label className="font-semibold">City Name</label>
        <div className="relative">
          <input
            type="text"
            className="bg-light--3 rounded-[5px] py-1 px-2 outline-none text-black duration-300 transition-all focus:bg-light--2 w-full"
            {...register('cityName')}
            disabled
          />
          {emoji && (
            <span className="absolute top-0 right-2 text-2xl">{emoji}</span>
          )}
        </div>
      </div>
      <div className="flex flex-col gap-1">
        <label className="font-semibold">
          When did you go to {cityName} de Alba?
        </label>
        <input
          type="date"
          className="bg-light--3 rounded-[5px] py-1 px-2 outline-none text-black duration-300 transition-all focus:bg-light--2"
          {...register('date')}
        />
      </div>
      <div className="flex flex-col gap-1">
        <label className="font-semibold">
          Notes about your trip to {cityName} de Alba
        </label>
        <textarea
          className="bg-light--3 rounded-[5px] py-1 px-2 outline-none text-black duration-300 transition-all focus:bg-light--2"
          {...register('notes')}
        />
      </div>

      <div className="flex items-center justify-between">
        <button className="bg-brand--2 rounded-[7px] border-none px-5 py-2 cursor-pointer hover:opacity-90 transition">
          Add
        </button>
        <button
          onClick={e => {
            e.preventDefault();
            router.push('/apps');
          }}
          className="border px-2.5 py-1.5 rounded-[7px] cursor-pointer font-semibold"
        >
          ← Back
        </button>
      </div>
    </form>
  );
}
