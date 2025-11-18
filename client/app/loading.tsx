import Spinner from './_components/Spinner';

export default function Loading() {
  return (
    <div className="h-[calc(100vh-3rem)] w-full top-0 left-0 flex items-center justify-center bg-dark--2 absolute z-10">
      <Spinner />
    </div>
  );
}
