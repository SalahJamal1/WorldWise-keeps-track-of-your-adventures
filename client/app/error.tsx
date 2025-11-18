'use client';

type Props = {
  error: unknown;
  reset: () => void;
};

export default function Error({ error, reset }: Props) {
  const message =
    (error as { message: string }).message || 'Something went wrong 😑';
  return (
    <div className="bg-dark--1 h-[calc(100vh-3rem)] flex flex-col items-center justify-center gap-4">
      <h2 className="text-3xl font-semibold tracking-wider text-red-600 text-center mx-10">
        {message}
      </h2>
      <button
        onClick={reset}
        className="bg-brand--2 text-xl font-medium px-3 py-1 rounded-full cursor-pointer"
      >
        Try again
      </button>
    </div>
  );
}
