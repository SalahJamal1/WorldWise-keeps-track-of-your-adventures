import { ReactNode } from 'react';
import Header from './Header';

type Props = {
  children: ReactNode;
};

export default function Main({ children }: Props) {
  return (
    <main className="bg-dark--1 h-[calc(100vh-3rem)]">
      <Header />
      {children}
    </main>
  );
}
