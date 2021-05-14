function resetCookieAndAlert() {
  setUserIDCookie();
  alert("ออกจากระบบแล้ว");
}

function getCookie(name) {
  const value = `; ${document.cookie}`;
  const parts = value.split(`; ${name}=`);
  if (parts.length === 2) return parts.pop().split(";").shift();
}

function setCookie(cname, cvalue) {
  document.cookie = cname + "=" + cvalue + ";" + "path =/";
}

function isLoggedIn() {
  if (getUserIDCookie() > 0) return true;
  else return false;
}

function getUserIDCookie() {
  const userID = getCookie("userID");
  return parseInt(userID);
}

function setUserIDCookie(cookieValue = -1) {
  setCookie("userID", cookieValue);
}