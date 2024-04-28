import { createAsyncThunk } from "@reduxjs/toolkit";
import { z } from "zod";
import { signInSchema, signUpSchema } from "@/types/validation";
import { instance } from "@/api/axiosInstance";
import Cookies from "js-cookie";

export const register = createAsyncThunk(
    "user/register",
    async (
        { email, password, lastName, firstName, phoneNumber }: z.infer<typeof signUpSchema>,
        { rejectWithValue },
    ) => {
        try {
            const response = await instance.post("/Auth/Register", {
                email,
                password,
                lastName,
                firstName,
                phoneNumber
            });

            return response.status;
        } catch (error: any) {
            if (error.response && error.response.data && error.response.data.errors) {
                const duplicateUserNameErrors = error.response.data.errors.DuplicateUserName;
                if (duplicateUserNameErrors && duplicateUserNameErrors.length > 0) {
                    console.log(duplicateUserNameErrors[0]);
                    return rejectWithValue(duplicateUserNameErrors[0]);
                }
            }

            console.error("An unexpected error occurred:", error);
            return rejectWithValue("An unexpected error occurred. Please try stronger password.");
        }
    },
);

export const login = createAsyncThunk(
    "user/login",
    async (
        { email, password }: z.infer<typeof signInSchema>,
        { rejectWithValue },
    ) => {
        try {
            const response = await instance.post("/Auth/Login", {
                email,
                password,
            });
            console.log(response)
            Cookies.set("jwt", response.data.token)
            return response;
        } catch (error: any) {
            if (error) {
                return rejectWithValue("Invalid credentials!");
            }
        }
    },
);
