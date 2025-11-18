'use client';
import CityForm from '@/app/_components/CityForm';

import WorkoutsForm from '@/app/_components/WorkoutsForm';
import { useState } from 'react';

export default function Page() {
  const [select, setSelect] = useState<string>('cities');

  return (
    <div className="w-full mt-10">
      <div className="flex flex-col bg-dark--2 py-6 px-8 mx-5">
        <select
          className="bg-light--3 rounded-[5px] px-2 py-1 text-black focus:bg-light--2 duration-300 transition-all ml-auto outline-none"
          value={select}
          onChange={e => setSelect(e.target.value)}
        >
          <option value="cities">Cities</option>
          <option value="workouts">Workouts</option>
        </select>
        {select === 'cities' ? <CityForm /> : <WorkoutsForm />}
      </div>
    </div>
  );
}
