import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "./globals.css";

const inter = Inter({
  variable: "--font-inter",
  subsets: ["latin"],
});

export const metadata: Metadata = {
  title: "Content Writer",
  description: "AI-assisted content generation for IT consulting projects",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en" className={`${inter.variable} light h-full antialiased`} style={{ colorScheme: "light" }}>
      <body className="min-h-full bg-[var(--color-bg)] text-[var(--color-text-primary)]">{children}</body>
    </html>
  );
}
