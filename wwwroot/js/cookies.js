function setUserID() {
  setCookie("userID", 0);
}

function getCookie(name) {
  const value = `; ${document.cookie}`;
  const parts = value.split(`; ${name}=`);
  if (parts.length === 2) return parts.pop().split(";").shift();
}

function setCookie(cname, cvalue) {
  document.cookie = cname + "=" + cvalue + ";" + "path =/";
}
