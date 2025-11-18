'use client';
import Spinner from '@/app/_components/Spinner';
import { useAuth } from '@/app/_hooks/useAuth';
import { GetCity } from '@/app/_lib/apiCity';
import { formatDate } from '@/app/utils/helper';
import Link from 'next/link';
import { useRouter } from 'next/navigation';
import { use, useEffect } from 'react';

type Props = {
  params: any;
};

export default function Page({ params }: Props) {
  const { dispatch, loading, city } = useAuth()!;
  const id: any = use(params);
  const router = useRouter();

  useEffect(() => {
    const controller = new AbortController();
    (async () => {
      dispatch({ type: 'LOADER' });
      try {
        const res = await GetCity(+id.cityId, controller.signal);

        dispatch({ type: 'LOAD_CITY', payload: res?.data });
      } catch (err) {
        console.log(err);
      }
    })();
    return () => controller.abort();
  }, [id, dispatch]);

  if (loading) return <Spinner />;

  return (
    <div className="w-full mt-10">
      <div className="flex flex-col items-start mx-8 bg-dark--2 rounded-[5px] px-8 py-5 space-y-8">
        <div>
          <span className="text-light--1">City Name</span>
          <h2 className="flex items-center gap-1 text-xl">
            <span className="text-4xl">{city?.emoji}</span>
            {city?.cityName}
          </h2>
        </div>
        <div className="flex flex-col">
          <span className="text-light--1">You went to Lisbon on</span>
          <span className="text-base font-medium">
            {city?.date && formatDate(city!.date)}
          </span>
        </div>

        <div className="flex flex-col">
          <span className="text-light--1 font-semibold">Learn more</span>
          <Link
            href={`https://en.wikipedia.org/wiki/${city?.cityName}`}
            className="block text-brand--1 text-base font-medium border-b w-fit"
          >
            Check out {city?.cityName} on Wikipedia →
          </Link>
        </div>
        <button
          onClick={() => router.back()}
          className="border px-2.5 py-1.5 rounded-[7px] text-base cursor-pointer mt-2 font-semibold"
        >
          ← Back
        </button>
      </div>
    </div>
  );
}
