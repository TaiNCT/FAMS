import { Action, Thunk, action, createStore, thunk } from "easy-peasy";
import axios from "axios";

// Store model
interface StoreModel {
	items: { id: number; name: string }[];
	setItems: Action<StoreModel, { id: number; name: string }[]>;
	saveItem: Thunk<StoreModel, void, unknown, Promise<void>>;
}

// @ts-ignore
const backend_api: string = `${import.meta.env.VITE_API_HOST}:${import.meta.env.VITE_API_PORT}`;

// Initial states
const initialState: StoreModel = {
	items: [],
	setItems: action((state, payload) => {
		state.items = payload;
	}),
	saveItem: thunk(async (actions, newItem, helpers) => {
		const { items } = helpers.getState();
		const source = axios.CancelToken.source();

		try {
			const response = await axios.post(`${backend_api}/items`, newItem, { cancelToken: source.token });
			actions.setItems([...items, response.data]);
		} catch (err) {
		}
	}),
};

const store = createStore(initialState);

export default store;
