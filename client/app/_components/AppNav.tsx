'use client';
import Link from 'next/link';
import { usePathname } from 'next/navigation';
export type Links = {
  label: string;
  href: string;
};
const appLinks: Links[] = [
  {
    label: 'cities',
    href: '/apps/cities',
  },
  {
    label: 'workouts',
    href: '/apps/workouts',
  },
];
export default function AppNav() {
  const pathname = usePathname();

  return (
    <ul className="flex mt-10 bg-[#242a2e] rounded-[5px] items-center py-1 overflow-hidden">
      {appLinks.map(link => (
        <li key={link.label}>
          <Link
            href={link.href}
            className={`px-4 text-xl capitalize hover:opacity-70 transition-all duration-300 py-1 ${
              pathname === link.href && 'bg-dark--2'
            }`}
          >
            {link.label}
          </Link>
        </li>
      ))}
    </ul>
  );
}
