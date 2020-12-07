import axios from "axios";
import qs from "qs";
import store from "../redux/store";

const instance = axios.create({
  baseURL: "/api/v1/",
  paramsSerializer: function (params) {
    return qs.stringify(params, { arrayFormat: "repeat" });
  },
});

instance.interceptors.request.use(function (config) {
  const { oidc } = store.getState();
  if (!oidc.isLoading && oidc.user) {
    config.headers.Authorization = `Bearer ${oidc.user.access_token}`;
  }

  return config;
});

export const storeAPI = {
  loadProducts: async (filters) => {
    const response = await instance.get("Products", {
      params: filters,
    });

    return response.data;
  },
  loadCategories: async () => {
    const response = await instance.get("Categories", {});
    return response.data;
  },
  loadCart: async (id) => {
    const response = await instance.get(`Cart/${id}`);
    return response.data;
  },
  addToCart: async (id, items) => {
    const response = await instance.post(`Cart/${id}`, { items });
    return response.data;
  },
  sendOrder: async (orderDetails) => {
    const response = await instance.post("Orders", orderDetails);
    return response.data;
  },
};

// const image =
//   "https://img2.freepng.ru/20180717/ifk/kisspng-smartphone-feature-phone-multimedia-app-mockup-psd-5b4eb748bb7aa7.3479269715318853847679.jpg";
//
// const products = [
//   { image, name: "Iphone", price: 500, categoryId: 1 },
//   { image, name: "Iphone 2g", price: 600, categoryId: 1 },
//   { image, name: "Iphone 3g", price: 700, categoryId: 1 },
//   { image, name: "Iphone 4", price: 800, categoryId: 1 },
//   { image, name: "Iphone 5", price: 850, categoryId: 1 },
//   { image, name: "Iphone 6", price: 860, categoryId: 1 },
//   { image, name: "Sport 1", price: 100, categoryId: 2 },
//   { image, name: "Sport 2", price: 150, categoryId: 2 },
//   { image, name: "Appliance 1", price: 4000, categoryId: 3 },
//   { image, name: "Appliance 2", price: 3000, categoryId: 3 },
// ];
//
// export const getProducts = (filters) => {
//   const { selectedCategories } = filters;
//   let result;
//   if (selectedCategories.length > 0) {
//     result = products.filter(
//       (p) => selectedCategories.indexOf(p.categoryId) >= 0
//     );
//   } else {
//     result = products;
//   }
//   return result;
// };
