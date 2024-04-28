import { ActionReducerMapBuilder, createSlice } from "@reduxjs/toolkit";
import Cookies from "js-cookie";
import { login, register } from "@/store/services/authService";
import {jwtDecode} from "jwt-decode";

interface IUserState {
    loading: boolean;
    token: string | null;
    user: any | null;
    error: string | null | undefined;
}

const accessToken: any = Cookies.get("jwt");

const initialState: IUserState = {
    loading: false,
    token: accessToken || null,
    user: accessToken ? jwtDecode(accessToken) : null,
    error: null,
};

const userSlice = createSlice({
    name: "user",
    initialState: initialState,
    reducers: {
        clearError(state) {
            state.error = null;
        },
        clearUser(state) {
            state.user = null;
        },
    },
    extraReducers: (builder: ActionReducerMapBuilder<IUserState>) => {
        builder
            .addCase(register.pending, (state) => {
                state.error = null;
            })
            .addCase(register.fulfilled, (state, action) => {
                state.error = null;
            })
            .addCase(register.rejected, (state, action) => {
                state.error = action.payload as string;
            })
            .addCase(login.pending, (state) => {
                state.loading = true;
                state.user = null;
                state.token = null;
                state.error = null;
            })
            .addCase(login.fulfilled, (state, action) => {
                console.log(action.payload)
                state.loading = false;
                state.user = jwtDecode(action?.payload?.data.token);
                state.token = action?.payload?.data.token;
                state.error = null;
            })
            .addCase(login.rejected, (state, action) => {
                state.loading = false;
                state.user = null;
                state.token = null;
                state.error = action.payload as string;
            });
    },
});
export const { clearError, clearUser } = userSlice.actions;
export default userSlice.reducer;
