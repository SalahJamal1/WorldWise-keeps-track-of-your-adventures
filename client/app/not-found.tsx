'use client';

import { useRouter } from 'next/navigation';

export default function NotFound() {
  const router = useRouter();
  return (
    <div className="bg-dark--1 h-[calc(100vh-3rem)] flex flex-col items-center justify-center gap-4">
      <h2 className="text-3xl font-semibold tracking-wider text-red-600">
        Something went wrong 😑
      </h2>
      <h4 className="text-2xl font-medium tracking-wider">
        We couldn’t find the page you’re looking for.
      </h4>
      <button
        onClick={() => router.back()}
        className="bg-brand--2 text-xl font-medium px-3 py-1 rounded-full cursor-pointer"
      >
        Go Back
      </button>
    </div>
  );
}
