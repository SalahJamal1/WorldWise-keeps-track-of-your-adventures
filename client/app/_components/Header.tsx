'use client';

import Link from 'next/link';
import Logo from './Logo';
import { usePathname } from 'next/navigation';
import { useAuth } from '../_hooks/useAuth';
import { useEffect, useState } from 'react';
import Loading from '../loading';
const navLinks: string[] = ['pricing', 'product', 'login'];

export default function Header() {
  const pathname = usePathname();
  const { auth } = useAuth()!;
  const [mount, setMount] = useState<boolean>(false);
  useEffect(() => {
    // eslint-disable-next-line react-hooks/set-state-in-effect
    setMount(true);
  }, []);
  if (!mount) return <Loading />;
  return (
    <header className="px-10 py-5">
      <nav className="flex items-center justify-between">
        <Logo />
        <ul className="flex gap-15">
          {navLinks.map(link => (
            <li key={link}>
              <Link
                href={`/${
                  link === 'login' ? (!auth ? 'login' : 'apps') : link
                }`}
                className={`text-xl capitalize tracking-widest cursor-pointer font-medium ${
                  pathname === `/${link}` && 'text-brand--2'
                } ${
                  link === 'login' &&
                  !auth &&
                  'bg-brand--2 px-6 py-2 rounded-[5px] hover:opacity-90 transition'
                }`}
              >
                {link === 'login' ? (auth ? 'Guest Area' : 'login') : link}
              </Link>
            </li>
          ))}
        </ul>
      </nav>
    </header>
  );
}
