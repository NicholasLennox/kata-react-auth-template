# Kata: React Authentication with Navbar

Build a React frontend for a JWT-based login and register flow. The backend is provided.

## Getting started

Fork this repository, then clone your fork:

```bash
git clone https://github.com/YOUR-USERNAME/REPO-NAME
cd REPO-NAME
```

The backend is ready to run. Check the backend README for setup instructions.

Reference implementation from class: https://github.com/NicholasLennox/react-auth-dotnet

## Requirements

- An `authService` that calls the API
- An `AuthContext` that stores `token` and `email`, and exposes `login`, `register`, and `logout`
- A `useAuth` hook that returns the context value (defined in `AuthContext.jsx`)
- A `Navbar` visible on every page - shows email + logout when logged in, login button when not
- `/register` and `/login` forms with validation and error feedback
- `/` as the home page, protected - unauthenticated users are redirected to `/login`
- Logout redirects to `/login` without calling `navigate` manually



## The backend

The backend is a .NET API included in the repository. Check the backend README for instructions on how to run it.

The API has three endpoints:

```
POST /api/register   { email, password }          → { token, email }
POST /api/login      { email, password }           → { token, email }
GET  /api/me         Authorization: Bearer <token> → { id, email }
```

Register and login both return a `token` and the user's `email`. You will store both.

To find the port your API is running on, check the terminal output when you start the .NET application.



## Project structure

The React app is already scaffolded in the repository. Clear out the default CSS and placeholder components, then build toward this structure:

```
src/
  components/
    Navbar.jsx
    ProtectedRoute.jsx
  context/
    AuthContext.jsx
  pages/
    Home.jsx
    Login.jsx
    Register.jsx
  services/
    authService.js
  App.jsx
  main.jsx
```



## Styling

Use whatever you like - plain CSS, CSS modules, Tailwind, Material UI, or Bootstrap.

If you want Bootstrap, add this to the `<head>` in `index.html`:

```html
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-sRIl4kxILFvY47J16cr9ZwB07vP4J8+LH7qKQnuqkuIAvNWLzeN8tE5YBujZqJLB" crossorigin="anonymous">
```



## Step 1: authService

Create `services/authService.js`.

Expose two functions: `login` and `register`. Both send a POST request with `{ email, password }` and return the parsed response body. If the response is not OK, throw an error - the calling code will decide what to show the user.



## Step 2: AuthContext

Create `context/AuthContext.jsx`.

Your context should hold `token` and `email`, and expose `login`, `register`, and `logout`.

Initialise both from `localStorage` so a page refresh does not log the user out. On login and register, store both values in state and `localStorage`. On logout, clear both.

Export a `useAuth` hook from the same file.



## Step 3: Forms

Build `Login.jsx` and `Register.jsx`.

Both forms should:

- Use a single `formData` state object for all fields
- Use a single `handleChange` function
- Validate that fields are not empty before submitting
- Show an error message if validation fails or the API call throws
- Redirect to `/` on success

Register should also check that the two password fields match. `confirmPassword` is for client-side validation only.



## Step 4: Navbar

Create `components/Navbar.jsx`.

Use `useAuth` to read current state.

- No token: show a Login button that navigates to `/login`
- Token present: show the user's email and a Logout button

Logout clears auth state. The protected route handles the redirect.



## Step 5: Layout with Outlet

You want the navbar on every page without copying it into each component. React Router's `Outlet` makes this clean.

Read this first: https://dev.to/jps27cse/understanding-layout-components-and-react-router-outlet-in-react-3l2e

Then use a layout route in `App.jsx` to wrap your pages so the navbar appears everywhere.



## Step 6: ProtectedRoute

Create `components/ProtectedRoute.jsx`.

If there is no token, redirect to `/login`. Otherwise render the children.



## Step 7: Home

`Home.jsx` is a simple protected page. Show something that confirms the user is logged in.



## Reflection

Answer briefly when you are done:

1. Why does `handleChange` use `[e.target.name]` instead of a separate function per field?
2. What is the difference between `token` living in React state vs `localStorage`? Why do we use both?
3. Why does logout redirect without calling `navigate`?
4. What does `<Outlet />` replace, and why is a layout route useful?
5. `confirmPassword` is used for validation in the form, but should not be sent to the API. Why?
