export interface Links {
  patch: Patch;
  reddit: Reddit;
  flickr: Flickr;
  presskit: string;
  webcast: string;
  youtubeId: string;
  article: string;
  wikipedia: string;
}

export interface Patch {
  small: string;
  large: string;
}

export interface Reddit {
  campaign: string;
  launch: string;
  media: string;
  recovery: string;
}

export interface Flickr {
  small: string[];
  original: string[];
}
