import Link from 'next/link';
import Header from './_components/Header';

export default function Home() {
  return (
    <div
      className={`[background-image:var(--linear-gradient),url(@/public/bg.jpg)] bg-center bg-cover h-[calc(100vh-3rem)]`}
    >
      <Header />
      <section className="h-[85%] flex flex-col items-center justify-center text-center">
        <h2 className="text-5xl font-medium leading-9">
          You travel the world.
        </h2>
        <h3 className="text-5xl font-medium leading-20 mb-1">
          WorldWise keeps track of your adventures.
        </h3>
        <p className="text-xl font-medium text-light--1 leading-8 mb-1 w-[90%]">
          A world map that tracks your footsteps into every city you can think
          of. Never forget your wonderful experiences, and show your friends how
          you have wandered the world.
        </p>
        <Link
          href="/apps"
          className=" text-xl capitalize tracking-widest cursor-pointer font-medium bg-brand--2 px-6 py-2 rounded-[5px] inline-block mt-5 hover:opacity-90 transition"
        >
          Start tracking now
        </Link>
      </section>
    </div>
  );
}
