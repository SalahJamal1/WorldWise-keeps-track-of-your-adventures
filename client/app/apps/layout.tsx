import { ReactNode } from 'react';
import SideBar from '../_components/SideBar';
import MapWrapper from '../_components/MapWrapper';
import User from '../_components/User';
import ProtectPage from '../_components/ProtectPage';

type Props = {
  children: ReactNode;
};
export default function Layout({ children }: Props) {
  return (
    <div className="grid grid-cols-[1fr_2fr] h-[calc(100vh-3rem)] relative">
      <ProtectPage>
        <SideBar>{children}</SideBar>
        <MapWrapper />
        <User />
      </ProtectPage>
    </div>
  );
}
