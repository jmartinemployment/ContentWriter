import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  // Served both directly and reverse-proxied under seo.geekatyourspot.com/content-writer.
  // Absolute prefix keeps _next/static asset URLs resolving to this app's own origin
  // regardless of which host the HTML was fetched through. Must be the stable Vercel
  // production alias (content-writer-eta.vercel.app), not a per-deployment hash URL —
  // the hash changes on every deploy and goes stale immediately.
  assetPrefix: "https://content-writer-eta.vercel.app",
};

export default nextConfig;
