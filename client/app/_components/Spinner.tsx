export default function Spinner() {
  return (
    <div
      className="w-20 h-20 rounded-full animate-spin 
  [background:conic-gradient(#0000_10%,var(--color-light--2))] 
  [-webkit-mask:radial-gradient(farthest-side,#0000_calc(100%-8px),#000_0)] my-auto"
    ></div>
  );
}
