import ApiAuthorzationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { Home } from "./components/Home";
import { Admin } from "./components/Admin";
import { Leaderboard } from "./components/Leaderboard";
import { League } from "./components/League";
import { Picks } from "./components/Picks";
import { Team } from "./components/Team";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/league',
    element: <League />
  },
  {
    path: '/team',
    element: <Team />
  },
  //{
  //  path: '/leaderboard',
  //  element: <Leaderboard />
  //},
  {
    path: '/picks',
    requireAuth: true,
    element: <Picks />
    },

  {
    path: '/admin',
    requireAuth: true,
    element: <Admin />
  },
  ...ApiAuthorzationRoutes
];

export default AppRoutes;
