import { Home } from "./components/Home";
import { MemberListing } from "./components/MemberListing";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
      path: 'member-listing',
      element: <MemberListing />
  }
];

export default AppRoutes;
