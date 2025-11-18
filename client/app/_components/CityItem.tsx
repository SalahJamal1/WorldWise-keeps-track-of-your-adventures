'use client';
import Link from 'next/link';
import { useAuth } from '../_hooks/useAuth';
import { deleteCity, GetCities } from '../_lib/apiCity';
import { ICITY } from '../utils/ICITY';

type Props = {
  city: ICITY;
};

export default function CityItem({ city }: Props) {
  const { dispatch } = useAuth()!;
  const handelDeleteCity = async (value: number) => {
    try {
      const res = await deleteCity(value);
      const data = await GetCities();
      dispatch({ type: 'LOAD_CITIES', payload: data?.data });
    } catch (err) {
      console.log(err);
    }
  };
  return (
    <Link href={`cities/${city.id}?lat=${city.lat}&lng=${city.lng}`}>
      <li className="flex px-4 py-2 bg-dark--2 items-center rounded-[5px] mb-4 mx-5 border-l-4 border-brand--2 cursor-pointer">
        <span className="text-3xl mr-2">{city.emoji}</span>
        <span className="text-xl flex-1">{city.cityName}</span>
        <span className="mr-3">
          (
          {new Date(city.date).toLocaleString('en-us', {
            year: '2-digit',
            month: 'long',
            day: '2-digit',
          })}
          )
        </span>
        <button
          onClick={e => {
            e.preventDefault();
            handelDeleteCity(city.id!);
          }}
          className="bg-[#242a2e] rounded-full text-xl w-6 h-6 flex items-center justify-center hover:bg-brand--1 duration-150 transition-all cursor-pointer"
        >
          &times;
        </button>
      </li>
    </Link>
  );
}
