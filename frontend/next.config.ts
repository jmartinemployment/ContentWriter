import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  // Served both directly and reverse-proxied under seo.geekatyourspot.com/content-writer.
  // Absolute prefix keeps _next/static asset URLs resolving to this app's own origin
  // regardless of which host the HTML was fetched through.
  assetPrefix: "https://content-writer-jeff-martins-projects-66716453.vercel.app",
};

export default nextConfig;
