import {
  Box,
  Button,
  CardMedia,
  Stack,
  TextField,
  Typography,
} from "@mui/material";
import { useFormik } from "formik";
// import { useDispatch, useSelector } from "react-redux";
import { Link, useNavigate } from "react-router-dom";
import * as Yup from "yup";
// import { images } from "../../Constants";
// import { login } from "../../Redux/authSlice";
import "./signIn.scss";

import images from "../../constant/images";
import api from "../../config/axios";
import { useDispatch } from "react-redux";
import { login } from "../../redux/features/authenSlice";
import { toast } from "react-toastify";

export default function SignIn() {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const formik = useFormik({
    initialValues: {
      username: "",
      password: "",
    },

    validationSchema: Yup.object({
      username: Yup.string()
        .required("required!!")
        .min(3, "at least 3 character")
        .max(20, "max 20 character"),
      password: Yup.string()
        .required("required!!")
        .min(1, "at least 3 character")
        .max(20, "max 20 character"),
    }),
  });

  const handleLogin = async (e) => {
    try {
      const response = await api.post("/authentication/login", {
        username: formik.values.username,
        password: formik.values.password,
      });
      localStorage.setItem("token", response.data.token);
      dispatch(login(response.data));
      console.log(response.data.role);
      if (response.data.role == "PARTY_HOST") {
        navigate("/dashboard/statistics");
      } else if (response.data.role === "ADMIN") {
        navigate("/dashboard");
      } else {
        navigate("/");
      }
    } catch (e) {
      toast.error(e.response.data);
      console.log(e);
    }
  };

  return (
    <Box
      className="bg_container-signin"
      style={{
        backgroundImage: `url(${images.login_bg2})`,
        backgroundSize: "cover",
        backgroundPosition: "center",
      }}
    >
      <CardMedia
        className="image-signin"
        component="img"
        src={images.cake}
        style={{ width: "40%" }}
        alt="images"
      />
      <Box className="form-signin">
        <Stack spacing={5}>
          <Typography textAlign="center" variant="h2" color="goldenrod">
            Sign In
          </Typography>

          <TextField
            className="textfield-signin"
            sx={{ width: "500px" }}
            id="username"
            name="username"
            label="User Name"
            type="text"
            value={formik.values.username}
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            error={formik.touched.username && Boolean(formik.errors.username)}
            helperText={formik.touched.username && formik.errors.username}
          />

          <TextField
            className="textfield-signin"
            sx={{ width: "500px" }}
            id="password"
            name="password"
            label="Password"
            type="password"
            value={formik.values.password}
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            error={formik.touched.password && Boolean(formik.errors.password)}
            helperText={formik.touched.password && formik.errors.password}
          />

          <Button
            className="Login_Button"
            sx={{ backgroundColor: "#626AD1", color: "white" }}
            onClick={handleLogin}
            disabled={!formik.isValid || formik.isSubmitting}
          >
            SIGN IN
          </Button>
          <Stack spacing={1}>
            <Typography textAlign="center" sx={{ color: "#526D82" }}>
              Dont have an account?{" "}
              <Link
                to="/register"
                style={{ color: "#0079FF", textDecoration: "none" }}
              >
                Register Now!
              </Link>
            </Typography>
            <Typography textAlign="center" sx={{ color: "#526D82" }}>
              Forgot password?{" "}
              <Link
                to="/resetpassword"
                style={{ color: "#0079FF", textDecoration: "none" }}
              >
                Click here to find your password
              </Link>
            </Typography>
            <Typography textAlign="center" sx={{ color: "#526D82" }}>
              Wanna back to homepage!{" "}
              <Link to="/" style={{ color: "#0079FF", textDecoration: "none" }}>
                Lets Go
              </Link>
            </Typography>
          </Stack>
        </Stack>
      </Box>
    </Box>
  );
}
