module.exports = {
  root: true,
  extends: ["@react-native-community", "prettier"],
  parser: "@typescript-eslint/parser",
  plugins: ["@typescript-eslint", "prettier"],
  overrides: [
    {
      files: ["*.ts", "*.tsx"],
      rules: {
        "@typescript-eslint/no-unused-vars": ["warn"],
        "no-shadow": "off",
        "no-undef": "off",
        "prettier/prettier": 0,
      },
    },
  ],
};
