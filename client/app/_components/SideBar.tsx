import { ReactNode } from 'react';
import Logo from './Logo';

type Props = {
  children: ReactNode;
};
export default function SideBar({ children }: Props) {
  return (
    <div className="bg-dark--1 flex flex-col items-center py-8">
      <Logo />
      {children}
      <p className="mt-auto text-light--2 text-base tracking-wider">
        © Copyright {new Date().getFullYear()} by WorldWise Inc.
      </p>
    </div>
  );
}
