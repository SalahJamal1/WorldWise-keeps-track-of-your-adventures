import Image from 'next/image';
import Main from '../_components/Main';

export default function Page() {
  return (
    <Main>
      <section className="grid grid-cols-2 max-w-4xl m-[60px_auto] items-center gap-x-18">
        <div className="relative aspect-square animate-blur">
          <Image src="/img-1.jpg" fill alt="img-1" />
        </div>
        <div className="space-y-10">
          <h2 className="text-4xl font-semibold">About WorldWide.</h2>
          <p className="text-light--1">
            Lorem ipsum dolor sit amet consectetur adipisicing elit. Illo est
            dicta illum vero culpa cum quaerat architecto sapiente eius non
            soluta, molestiae nihil laborum, placeat debitis, laboriosam at fuga
            perspiciatis?
          </p>
          <p className="text-light--1">
            Lorem, ipsum dolor sit amet consectetur adipisicing elit. Corporis
            doloribus libero sunt expedita ratione iusto, magni, id sapiente
            sequi officiis et.
          </p>
        </div>
      </section>
    </Main>
  );
}
