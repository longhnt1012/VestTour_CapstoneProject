<<<<<<< HEAD
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
=======
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
>>>>>>> 3a4e369de231668637d0e5607694b9a8f9967a48
import { routes } from "./routes/paths";

function App() {
  return (
    <Router>
      <Routes>
        {routes.map((route, index) => (
          <Route key={index} path={route.path} element={route.element}>
<<<<<<< HEAD
            {route.children &&
              route.children.map((child, childIndex) => (
                <Route
                  key={childIndex}
                  path={child.path}
                  element={child.element}
                />
              ))}
=======
            {route.children && route.children.map((childRoute, childIndex) => (
              <Route 
                key={childIndex} 
                path={childRoute.path} 
                element={childRoute.element} 
              />
            ))}
>>>>>>> 3a4e369de231668637d0e5607694b9a8f9967a48
          </Route>
        ))}
      </Routes>
    </Router>
  );
}

export default App;
