import { Crew } from "./crew";
import { Links } from "./links";
import { Launchpad } from "./launchpad";
import { Rocket } from "./rocket";

export interface CustomLaunch {
  flight_number: number;
  name: string;
  date_utc: Date;
  rocket: Rocket;
  success: boolean | null;
  details: string;
  links: Links;
  crew: Crew[];
  launchpad: Launchpad;
  id: string;
}
