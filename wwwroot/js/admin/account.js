function isAdmin(userID = -1) {
  if (userID !== -1) {
    if (userID >= 6 && userID <= 10) return true;
    else return false;
  }
}
