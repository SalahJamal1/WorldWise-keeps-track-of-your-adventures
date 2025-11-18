import Image from 'next/image';
import Main from '../_components/Main';

export default function Page() {
  return (
    <Main>
      <section className="grid grid-cols-[1fr_1fr] m-[60px_auto] max-w-4xl gap-x-18">
        <div className="space-y-8 flex flex-col justify-center items-center">
          <h2 className="text-4xl leading-15 font-semibold">
            Simple pricing. Just $9/month.
          </h2>
          <p className="text-base leading-7 text-light--1">
            Lorem ipsum dolor, sit amet consectetur adipisicing elit. Vitae vel
            labore mollitia iusto. Recusandae quos provident, laboriosam fugit
            voluptatem iste.
          </p>
        </div>
        <div className="w-full relative aspect-square animate-blur">
          <Image src="/img-2.jpg" alt="img-1" fill />
        </div>
      </section>
    </Main>
  );
}
